import { BrowserRouter } from "react-router-dom";
import { ThemeProvider } from "@mui/material/styles";
import { AppContextProvider } from "./app/contexts/AppContext";

import CssBaseline from "@mui/material/CssBaseline";
import theme from "./styles/theme";
import Routes from "./Routes";

const App = () => {
  return (
    <BrowserRouter>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <AppContextProvider>
          <Routes />
        </AppContextProvider>
      </ThemeProvider>
    </BrowserRouter>
  );
};

export default App;
