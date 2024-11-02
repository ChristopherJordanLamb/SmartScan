# Gaussian Splatting Steps
##Install Required Software

Install COLMAP for Windows.
Install Meshroom version 2021.1.1 for Windows.
##Setup Environment

Place the downloaded Meshroom folder and the gaussian-splatting-windows.py file (found in the Gaussian Splatting folder on Gitea) in the same directory.
Run the Script

Execute the gaussian-splatting-windows.py script and provide the path to your video when prompted in the terminal.
##Upload Data

Upload the generated “Data” folder to your Google Drive.
##Training

Open the training.ipynb notebook (found in the Gaussian Splatting folder on Gitea) on Google Colab with T4 GPU runtime (free) and run all cells.
Connect to your Google Drive when prompted, then provide the path to the “Data” folder.
Download Output

After training, locate and download your point cloud folder from gaussian-splatting/output/.

