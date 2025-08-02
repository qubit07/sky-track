# Its recommend installing and updating Picamera2 using apt which will avoid compatibility problems
sudo apt-get update
sudo apt install -y python3-picamera2 --no-install-recommends

python -m venv .venv --system-site-packages
source .venv/bin/activate