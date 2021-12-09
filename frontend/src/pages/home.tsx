import { Navigate } from "react-router-dom";
import { HomePage } from "../features/home";
import { useAppContext } from "../app/hooks/useAppContext";

const Home = () => {
  const { context } = useAppContext();
  const { user } = context;

  if (user.isAuthenticated) {
    return <Navigate to="/dashboard" />;
  }

  return <HomePage />;
};

export default Home;
