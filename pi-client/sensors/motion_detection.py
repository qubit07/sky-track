# motion_detection.py
# This module implements motion detection using OpenCV.
import cv2

class MotionDetection:
    def __init__(self, threshold=30, min_area=500):
        self.previous_frame = None
        self.threshold = threshold
        self.min_area = min_area

    def detect(self, frame):
        # convert the frame to grayscale and apply Gaussian blur
        gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
        gray = cv2.GaussianBlur(gray, (21, 21), 0)
        
        if self.previous_frame is None:
            self.previous_frame = gray
            return False
        
        # compute the absolute difference between the current frame and previous frame
        frame_delta = cv2.absdiff(self.previous_frame, gray)
        self.previous_frame = gray

        # threshold the delta image
        thresh = cv2.threshold(frame_delta, self.threshold, 255, cv2.THRESH_BINARY)[1]
        thresh = cv2.dilate(thresh, None, iterations=2)

        # find contours in the thresholded image
        contours, _ = cv2.findContours(thresh.copy(), cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)
        for contour in contours:
            # if we found a contour that is large enough, motion is detected
            if cv2.contourArea(contour) >= self.min_area:
                return True
        # no significant motion detected    
        return False