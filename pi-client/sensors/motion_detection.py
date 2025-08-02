# motion_detection.py
# This module implements motion detection using OpenCV.
from picamera2 import Picamera2
import numpy as np
import time

class MotionDetection:
    def __init__(self, threshold=10):
        self.previous_frame = None
        self.threshold = threshold

    def detect(self, frame):
        # convert the frame to grayscale
        gray = np.mean(frame, axis=2).astype(np.uint8) 
        
        if self.previous_frame is None:
            self.previous_frame = gray
            return False
        
        # compute the absolute difference between the current frame and previous frame
        diff = np.abs(gray.astype(np.int16) - self.previous_frame.astype(np.int16))
        motion_level = np.mean(diff)
        
        self.previous_frame = gray

        if motion_level > self.threshold:
            print(f"Motion detected with level: {motion_level}")
            return True
        
        return False