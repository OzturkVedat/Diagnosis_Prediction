from flask import Flask, request, jsonify       # Python Flask API
import joblib

app = Flask(__name__)
model = joblib.load('path/to/yourFile.pkl')     # load the pkl file from the same directory

@app.route('/predict', methods=['POST'])
def predict():
    data = request.json
    features = [data['Age'], data['Gender'], data['ChestPainType'], data['MaxHearthRate'],
                data['RestingBloodPressure'],data['SerumCholesterol'],data['FastingBloodSugar'],data['RestingElectrocardio'],
                data['ExerciseAngina'],data['OldPeak'],data['SlopeST'],data['MajorVessels']]  

    prediction = model.predict([features])

    return jsonify({'prediction': int(prediction[0])})

if __name__ == '__main__':
    app.run(port=5000)  # Adjust the port as needed
