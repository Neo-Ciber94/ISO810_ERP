import { useLocation, Navigate } from "react-router-dom";
import { useAppContext } from "./app/hooks/useAppContext";

const ProtectedRoute = ({ children }: { children: JSX.Element }) => {
  const location = useLocation();
  const { context } = useAppContext();
  const { user } = context;

  if (!user.isAuthenticated) {
    return <Navigate to="/" state={{ from: location }} />;
  }

  return children;
};

export default ProtectedRoute;
