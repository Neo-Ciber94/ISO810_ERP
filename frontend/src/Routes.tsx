import { lazy, Suspense } from "react";
import { Routes, Route } from "react-router-dom";

import Loading from "./app/components/layout/components/Loading";

const HomePage = lazy(() => {
  return import("./pages/home");
});

const DashboardPage = lazy(() => {
  return import("./pages/dashboard");
});

const AppRoutes = () => {
  return (
    <Routes>
      <Route
        path="/"
        element={
          <Suspense fallback={<Loading />}>
            <HomePage />
          </Suspense>
        }
      />

      <Route
        path="dashboard"
        element={
          <Suspense fallback={<Loading />}>
            <DashboardPage />
          </Suspense>
        }
      />

      <Route
        path="*"
        element={
          <Suspense fallback={<Loading />}>
            <h1>Page Not Found</h1>
          </Suspense>
        }
      />
    </Routes>
  );
};

export default AppRoutes;
