import paho.mqtt.client as mqtt
import logging
from mqtt_topics import MqttTopics

logger = logging.getLogger(__name__) 

class MqttClient:
    def __init__(self, host='localhost', port=1883, client_id='pi-client'):
        self.host = host
        self.port = port
        self.client_id = client_id
        self.client = mqtt.Client(client_id=self.client_id)
        self.connected = False
        self.client.on_connect = self._on_connect
        self.client.on_disconnect = self._on_disconnect
        self.client.on_message = self._on_message

    def _on_connect(self):
        self.connected = True
        logger.info(f"Connected to MQTT broker at {self.host}:{self.port} with client ID {self.client_id}")
        self.subscribe(MqttTopics.CAMERA_CONTROL)
        self.subscribe(MqttTopics.CAMERA_STATE)

    def _on_disconnect(self):
        self.connected = False
        logger.warn("Disconnected from MQTT broker")

    def _on_message(self, client, userdata, msg):
        logger.info(f"Received message: {msg.payload.decode()} on topic {msg.topic}")

    def publish(self, topic: MqttTopics, message: str):
        if not self.connected:
            raise Exception("Client is not connected to the broker")
        logger.info(f"Published message '{message}' to topic '{topic}'")

    def subscribe(self, topic: MqttTopics):
        if not self.connected:
            raise Exception("Client is not connected to the broker")
        self.client.subscribe(topic)
        logger.info(f"Subscribed to topic '{topic}'")

    def unsubscribe(self, topic: MqttTopics):
        if not self.connected:
            raise Exception("Client is not connected to the broker")
        self.client.unsubscribe(topic)
        logger.info(f"Unsubscribed from topic '{topic}'")

    def start(self):
        self.client.connect(self.host, self.port)
        self.client.loop_start()

    def stop(self):
        self.client.loop_stop()
        self.client.disconnect()