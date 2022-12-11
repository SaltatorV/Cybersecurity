import React, { useState } from "react";
import { Form, FormGroup, Label, Input } from "reactstrap";

const LoginForm = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const [error, setError] = useState("");

  const handleSubmit = async (event) => {
    event.preventDefault();

    await fetch("http://localhost:7277/api/account/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
      },
      credentials: "include",
      body: JSON.stringify({
        login: username,
        password: password,
      }),
    })
      .then((response) => {
        if (response.status !== 200) {
          setError("Check email or password");
        } else {
          return response.json();
        }
      })
      .then((res) => {
        if (res._isSetOneTimePassword === true) {
          window.location.href = "/changepassword";
        } else {
          window.location.href = "/";
        }
      })
      .catch((error) => console.log(error));
  };

  return (
    <Form onSubmit={handleSubmit}>
      <FormGroup>
        <Label for="username">Username:</Label>
        <Input
          id="username"
          type="text"
          value={username}
          size="lg"
          onChange={(event) => setUsername(event.target.value)}
        />
      </FormGroup>

      <FormGroup>
        <Label for="password">Password:</Label>
        <Input
          id="password"
          type="password"
          value={password}
          size="lg"
          onChange={(event) => setPassword(event.target.value)}
        />
      </FormGroup>
      <FormGroup>{error}</FormGroup>
      <button className="btn btn-secondary" type="submit">
        Log In
      </button>
    </Form>
  );
};

export default LoginForm;
