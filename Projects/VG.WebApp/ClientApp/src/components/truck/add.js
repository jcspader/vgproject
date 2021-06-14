import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { useForm } from "react-hook-form";

import TruckDataService from "../../services/truck.services";
import ModelDataService from "../../services/model.services";

export default function Add() {
  const [Models, setModels] = useState([]);
  const [submitted, setSubmitted] = useState(false);
  const [erroMessage, setErroMessage] = useState();

  const onsaveTruck = (data) => {
    console.warn(data);
    setSubmitted(true);

    TruckDataService.create(data)
      .then((response) => {})
      .catch((e) => {
        setErroMessage(e.response.data);
      });
  };

  const retrieveModels = () => {
    ModelDataService.getAll()
      .then((response) => {
        setModels(response.data);
      })
      .catch((e) => {
        setErroMessage(e.response.data);
      });
  };

  useEffect(() => {
    retrieveModels();
  }, []);

  const {
    register,
    handleSubmit,
    getValues,
    formState: { errors },
  } = useForm();

  console.log(errors);

  return (
    <div className="submit-form">
      {submitted ? (
        <div>
          <h4>You submitted successfully!</h4>

          <Link to={"/trucks"} className="btn btn-success">
            Return to List
          </Link>
        </div>
      ) : (
        <div>
          <h4>Create a new Truck</h4>

          {erroMessage && (
            <div className="alert alert-danger" role="alert">
              {erroMessage}
            </div>
          )}

          <form onSubmit={handleSubmit(onsaveTruck)}>
            <div className="form-group">
              <label htmlFor="modelId">Model</label>
              <select
                className={`form-control ${errors.modelId ? "is-invalid" : ""}`}
                placeholder="Models"
                {...register("modelId", { required: true, value: "" })}
              >
                <option disabled value="">
                  Models
                </option>
                {Models &&
                  Models.map((Model, index) => (
                    <option key={Model.id} value={Model.id}>
                      {Model.name}
                    </option>
                  ))}
              </select>
              {errors.modelId && (
                <div className="invalid-feedback">
                  {errors.modelId?.type === "required" &&
                    "This field is required"}
                </div>
              )}
            </div>

            <div className="form-group">
              <div className="row">
                <div className="col">
                  <label htmlFor="manufactureYear">Manufacture Year</label>
                  <input
                    type="number"
                    className={`form-control ${
                      errors.manufactureYear ? "is-invalid" : ""
                    }`}
                    placeholder="YYYY"
                    {...register("manufactureYear", {
                      valueAsNumber: true,
                      required: true,
                      min: 1980,
                      max: new Date().getFullYear + 1,
                    })}
                  />
                  {errors.manufactureYear && (
                    <div className="invalid-feedback">
                      {errors.manufactureYear?.type === "required" &&
                        "This field is required"}
                      {errors.manufactureYear?.type === "min" &&
                        "The mininum value is 1980"}
                      {errors.manufactureYear?.type === "max" &&
                        `The max value is ${new Date().getFullYear + 1}`}
                    </div>
                  )}
                </div>
                <div className="col">
                  <label htmlFor="modelYear">Model Year</label>
                  <input
                    type="number"
                    className={`form-control ${
                      errors.modelYear ? "is-invalid" : ""
                    }`}
                    placeholder="YYYY"
                    {...register("modelYear", {
                      required: true,
                      valueAsNumber: true,

                      validate: (value) => {
                        const inputManufacture = getValues("manufactureYear");
                        return (
                          value === inputManufacture ||
                          value === inputManufacture + 1
                        );
                      },
                    })}
                  />
                  {errors.modelYear && (
                    <div className="invalid-feedback">
                      {errors.modelYear?.type === "required" &&
                        "This field is required"}
                      {errors.modelYear?.type === "validate" &&
                        "The value must be equal or after 1 year than Manufacture Year"}
                    </div>
                  )}
                </div>
              </div>
            </div>
            <div className="form-group">
              <label htmlFor="color">Color</label>
              <input
                type="text"
                className={`form-control ${errors.color ? "is-invalid" : ""}`}
                placeholder="ex. Blue, Black, White"
                {...register("color", { maxLength: 15 })}
              />
              {errors.color && (
                <div className="invalid-feedback">
                  {errors.color?.type === "maxLength" &&
                    "The max length is 15 characters"}
                </div>
              )}
            </div>
            <div className="form-group">
              <label htmlFor="price">Price</label>
              <div className="input-group">
                <div className="input-group-prepend">
                  <div className="input-group-text">R$</div>
                </div>
                <input
                  type="text"
                  className={`form-control ${errors.price ? "is-invalid" : ""}`}
                  placeholder="999.99"
                  {...register("price", {
                    pattern: {
                      value: /^[0-9]+(\.[0-9]{1,2})?$/,
                      message: "Use format 999.99",
                    },
                  })}
                />
                {errors.price && (
                  <div className="invalid-feedback">{errors.price.message}</div>
                )}
              </div>
            </div>
            <button type="submit" className="btn btn-success">
              Create
            </button>
          </form>
        </div>
      )}
    </div>
  ); //return
}
