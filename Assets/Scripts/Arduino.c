#define SERIAL_USB
int redLed = 12;
int greenLed = 11;
int smokeA0 = A0;
int smokeA1 = A1;
int aux = 0;
//threshold value
int sensorThres = 350;
String data;
String holder;
void setup() {
#ifdef NATIVE_USB
  Serial.begin(1); //Baudrate is irevelant for Native USB
#endif
  pinMode(redLed, OUTPUT);
  pinMode(greenLed, OUTPUT);
  pinMode(smokeA0, INPUT);
  pinMode(smokeA1, INPUT);
#ifdef SERIAL_USB
  Serial.begin(50000); // You can choose any baudrate, just need to also change it in Unity.
  while (!Serial); // wait for Leonardo enumeration, others continue immediately
#endif
}

void loop() {
  if (aux == 0) {
    holder = "1,";
    data = holder + analogRead(smokeA0);
    sendData(data);
    ++aux;
  }
  else
  {
    holder = "2,";
    data = holder + analogRead(smokeA1);
    sendData(data);
    --aux;
  }
  if (analogRead(smokeA0) > sensorThres || analogRead(smokeA1) > sensorThres)
  {
    digitalWrite(redLed, HIGH);
    digitalWrite(greenLed, LOW);
  }
  else
  {
    digitalWrite(redLed, LOW);
    digitalWrite(greenLed, HIGH);
  }
  delay(10); // Choose your delay having in mind your ReadTimeout in Unity3D
}

void sendData(String data) {
#ifdef NATIVE_USB
  Serial.println(data); // need a end-line because wrmlh.csharp use readLine method to receive data
#endif

#ifdef SERIAL_USB
  Serial.println(data); // need a end-line because wrmlh.csharp use readLine method to receive data
#endif
}