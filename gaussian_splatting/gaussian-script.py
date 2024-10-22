import subprocess
import os

def run_command(command):
    try:
        print(f"Executing: {command}")
        subprocess.run(command, shell=True, check=True)
        print(f"Completed: {command}\n")
    except subprocess.CalledProcessError as e:
        print(f"Error occurred while executing: {command}\nError: {e}\n")


def get_video_path():
    while True:
        # Ask for the video path
        video_path = input("Enter the path to the 360 video file (or press 'q' to quit): ")

        # Check if the user wants to quit
        if video_path.lower() == 'q':
            print("Exiting...")
            return None

        # Check if the provided path exists
        if os.path.exists(video_path):
            return video_path
        else:
            print(f"Error: The video file at '{video_path}' was not found. Please provide the correct path.\n")


def main():
    # Step 1: Get video path input from user and check if file exists
    video_path = get_video_path()

    if video_path is None:
        return  # Exit the script if the user presses 'q'

    # Step 2: Create the 'image_360' directory
    run_command("mkdir image_360")

    # Step 3: Extract frames from the 360 video using ffmpeg
    fps = 3  # Example: Adjust the fps value as needed
    output_folder = "./image_360"
    run_command(f"ffmpeg -i {video_path} -vf fps={fps} -qscale:v 1 {output_folder}/image_%04d.jpg")

    # Step 4: Create the 'images' directory
    run_command("mkdir images")

    # Step 5: Run Meshroom's aliceVision_utils_split360Images
    run_command("./Meshroom-2021.1.0-av2.4.0-centos7-cuda10.2/aliceVision/bin/aliceVision_utils_split360Images "
                "-i ./image_360 -o ./images --equirectangularNbSplits 8 --equirectangularSplitResolution 1200")

    # Step 6: Create 'model' folder and move 'images' into 'input'
    run_command("mkdir -p ./gaussian-splatting/data/model")
    run_command("mv images ./gaussian-splatting/data/model/input")

    # Step 7: Navigate to 'gaussian-splatting' directory
    run_command("cd gaussian-splatting")

    # Step 8: Activate conda environment
    run_command("conda activate gaussian-splatting")

    # Step 9: Run 'converty.py' and 'train.py' scripts
    run_command("python3 converty.py -s ./data/model/input")
    run_command("python3 train.py -s ./data/model")


if __name__ == "__main__":
    main()
