import axios from "axios";

const http = axios.create({
  baseURL: "http://localhost:2536/api/",
  timeout: 1000,
});

export default http;
