import axios from "axios";

const IS_DEV: boolean = !process.env.NODE_ENV || process.env.NODE_ENV === 'development';
const BASE_URL = IS_DEV ? "http://localhost:5000/api" : "http://143.198.178.55:3000/api";

export const axiosInstance = axios.create({
  baseURL: BASE_URL,
  withCredentials: true,
  headers: {
    Accept: "application/json",
    "Content-Type": "application/json",
  },
});
