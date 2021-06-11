import http from "./service.config";

class ModelDataService {
  getAll() {
    return http.get("model");
  }

  get(id) {
    return http.get(`model/${id}`);
  }

  create(data) {
    return http.post("model", data);
  }

  update(id, data) {
    return http.put(`model/${id}`, data);
  }

  delete(id) {
    return http.delete(`model/${id}`);
  }
}

export default new ModelDataService();
