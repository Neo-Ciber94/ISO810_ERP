import { lazy, Suspense, useEffect } from "react";
import { useAppContext } from "./app/hooks/useAppContext";
import { axiosInstance } from "./app/config";
import { Routes, Route } from "react-router-dom";

import Loading from "./app/components/layout/components/Loading";
import ProtectedRoute from "./ProtectedRoute";

const HomePage = lazy(() => {
  return import("./pages/home");
});

const DashboardPage = lazy(() => {
  return import("./pages/dashboard");
});

const ActivityPage = lazy(() => {
  return import("./pages/activity");
});

const ServicePage = lazy(() => {
  return import("./pages/service");
});

const OptionsPage = lazy(() => {
  return import("./pages/options");
});

const OrganizationPage = lazy(() => {
  return import("./pages/organization/index");
});

const OrganizationFormPage = lazy(() => {
  return import("./pages/organization/form");
});

const AppRoutes = () => {
  const { updateUserData } = useAppContext();

  useEffect(() => {
    // Check if user is authenticated
    axiosInstance
      .get("/Account/me")
      .then((response) => {
        const { data } = response;
        updateUserData({ user: { ...data, isAuthenticated: true } });
      })
      .catch((error) => {
        if (error) {
          updateUserData({ user: { isAuthenticated: false } });
        }
      });
  }, []);

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
            <ProtectedRoute>
              <DashboardPage />
            </ProtectedRoute>
          </Suspense>
        }
      />

      <Route
        path="activity"
        element={
          <Suspense fallback={<Loading />}>
            <ProtectedRoute>
              <ActivityPage />
            </ProtectedRoute>
          </Suspense>
        }
      />

      <Route
        path="service"
        element={
          <Suspense fallback={<Loading />}>
            <ProtectedRoute>
              <ServicePage />
            </ProtectedRoute>
          </Suspense>
        }
      />

      <Route
        path="options"
        element={
          <Suspense fallback={<Loading />}>
            <ProtectedRoute>
              <OptionsPage />
            </ProtectedRoute>
          </Suspense>
        }
      />

      <Route
        path="organization"
        element={
          <Suspense fallback={<Loading />}>
            <ProtectedRoute>
              <OrganizationPage />
            </ProtectedRoute>
          </Suspense>
        }
      />

      <Route
        path="organization/form"
        element={
          <Suspense fallback={<Loading />}>
            <ProtectedRoute>
              <OrganizationFormPage />
            </ProtectedRoute>
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
