from enum import Enum

class MqttTopics(Enum):
    CAMERA_STATE = "camera/state"
    CAMERA_CONTROL = "camera/control"
    SYSTEM_STATUS = "system/status"
    SYSTEM_CONTROL = "system/control"
    ALERTS = "alerts"

    def __str__(self):
        return self.value   