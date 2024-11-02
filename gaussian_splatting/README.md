# Gaussian Splatting Steps

## 1. Install Required Software
- Install COLMAP for Windows.
- Install Meshroom version 2021.1.1 for Windows.

## 2. Setup Environment
- Place the downloaded Meshroom folder and the `gaussian-splatting-windows.py` file (found in the Gaussian Splatting folder on Gitea) in the same directory.

![alt text](https://gitea-cert.cdirmit.co/2024S2_Projects/SmartScan24/raw/branch/gaussian-splatting/gaussian_splatting/Readme-Images/samefolder.png?raw=true)

## 3. Run the Script
- Execute the `gaussian-splatting-windows.py` script and provide the path to your video when prompted in the terminal.

## 4. Upload Data
- Upload the generated “Data” folder to your Google Drive.

![alt text](https://gitea-cert.cdirmit.co/2024S2_Projects/SmartScan24/raw/branch/gaussian-splatting/gaussian_splatting/Readme-Images/data.png?raw=true)

## 5. Training
- Open the `training.ipynb` notebook (found in the Gaussian Splatting folder on Gitea) on Google Colab with T4 GPU runtime (free) and run all cells.
- Connect to your Google Drive when prompted, then provide the path to the “Data” folder.

## 6. Download Output
- After training, locate and download your point cloud folder from `gaussian-splatting/output/`.

![alt text](https://gitea-cert.cdirmit.co/2024S2_Projects/SmartScan24/raw/branch/gaussian-splatting/gaussian_splatting/Readme-Images/output.png?raw=true)
