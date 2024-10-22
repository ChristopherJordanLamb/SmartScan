import subprocess

def run_command(command):
    try:
        print(f"Executing: {command}")
        subprocess.run(command, shell=True, check=True)
        print(f"Completed: {command}\n")
    except subprocess.CalledProcessError as e:
        print(f"Error occurred while executing: {command}\nError: {e}\n")


def main():
    # Step 1: Create the 'image_360' directory
    run_command("mkdir image_360")

    # Step 2: Extract frames from the 360 video using ffmpeg
    # Replace 'path/to/360_video.mp4', 'fps', and 'output_folder' with the actual values
    video_path = "path/to/360_video.mp4"
    fps = 30  # Example: Adjust the fps value as needed
    output_folder = "./image_360"
    run_command(f"ffmpeg -i {video_path} -vf fps={fps} -qscale:v 1 {output_folder}/image_%04d.jpg")

    # Step 3: Create the 'images' directory
    run_command("mkdir images")

    # Step 4: Run Meshroom's aliceVision_utils_split360Images
    run_command("./Meshroom-2021.1.0-av2.4.0-centos7-cuda10.2/aliceVision/bin/aliceVision_utils_split360Images "
                "-i ./image_360 -o ./images --equirectangularNbSplits 8 --equirectangularSplitResolution 1200")

    # Step 5: Create 'model' folder and move 'images' into 'input'
    run_command("mkdir -p ./gaussian-splatting/data/model")
    run_command("mv images ./gaussian-splatting/data/model/input")

    # Step 6: Navigate to 'gaussian-splatting' directory
    run_command("cd gaussian-splatting")

    # Step 7: Activate conda environment
    run_command("conda activate gaussian-splatting")

    # Step 8: Run 'converty.py' and 'train.py' scripts
    run_command("python3 converty.py -s ./data/model/input")
    run_command("python3 train.py -s ./data/model")


if __name__ == "__main__":
    main()
