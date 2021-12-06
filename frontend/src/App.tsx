import React, { useEffect } from "react";
import logo from "./logo.svg";
import "./App.css";

const API_URL = "http://localhost:5000/api/weatherforecast/";

const fetchJson = async (url: string) => {
  await delay(1000); // simulate server latency

  const response = await fetch(url);
  return response.json();
};

const delay = (ms: number) => new Promise((resolve) => setTimeout(resolve, ms));

interface WeatherForecast {
  date: Date;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

function App() {
  const [forecasts, setForecasts] = React.useState<WeatherForecast[]>([]);
  const [isLoading, setIsLoading] = React.useState(false);

  useEffect(() => {
    setIsLoading(true);

    const run = async () => {
      try {
        const forecasts = await fetchJson(API_URL);
        setForecasts(forecasts);
      } catch (err) {
        console.error(err);
      } finally {
        setIsLoading(false);
      }
    };

    // Run the async function
    run();
  }, []);

  return (
    <div className="App">
      <header className="App-header">
        {isLoading && <div>Loading...</div>}
        {!isLoading && (
          <div className="fade-in">
            <img src={logo} className="App-logo" alt="logo" />
            <pre style={{ textAlign: "left", fontSize: 15 }}>
              {JSON.stringify(forecasts, null, 2)}
            </pre>
          </div>
        )}
      </header>
    </div>
  );
}

export default App;
