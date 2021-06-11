import http from "./service.config";

class TruckDataService {
  getAll() {
    return http.get("truck");
  }

  get(id) {
    return http.get(`truck/${id}`);
  }

  create(data) {
    return http.post("truck", data);
  }

  update(id, data) {
    return http.put(`truck/${id}`, data);
  }

  delete(id) {
    return http.delete(`truck/${id}`);
  }
}

export default new TruckDataService();
