import React, { useState } from "react";
import { Link } from "react-router-dom";
import { useForm } from "react-hook-form";

import ModelDataService from "../../services/model.services";

export default function Add() {
  const [submitted, setSubmitted] = useState(false);
  const [erroMessage, setErroMessage] = useState();

  const onsaveModel = (data) => {
    console.warn(data);
    setSubmitted(true);

    ModelDataService.create(data)
      .then((response) => {
        console.warn(response);
      })
      .catch((e) => {
        if (e.response?.data) setErroMessage(e.response.data);
      });
  };

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm();

  console.log(errors);

  return (
    <div className="submit-form">
      {submitted ? (
        <div>
          <h4>You submitted successfully!</h4>

          <Link to={"/Models"} className="btn btn-success">
            Return to List
          </Link>
        </div>
      ) : (
        <div>
          <h4>Create a new Model</h4>

          {erroMessage && (
            <div className="alert alert-danger" role="alert">
              {erroMessage}
            </div>
          )}

          <form onSubmit={handleSubmit(onsaveModel)}>
            <div className="form-group">
              <label htmlFor="color">Name</label>
              <input
                type="text"
                className={`form-control ${errors.name ? "is-invalid" : ""}`}
                {...register("name", { maxLength: 15 })}
              />
              {errors.name && (
                <div className="invalid-feedback">
                  {errors.name?.type === "maxLength" &&
                    "The max length is 15 characters"}
                </div>
              )}
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
