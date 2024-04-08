import styled, { css } from "styled-components";

export const Button = styled.button<{
  $primary?: boolean;
  $animation?: boolean;
  $line?: boolean;
  $px?: number;
  $py?: number;
  $danger?: boolean;
  $transparent?: boolean;
}>`
  background-color: ${(props) =>
    props.$primary
      ? "#93c5fd"
      : props.$danger
      ? "red"
      : props.$transparent
      ? "transparent"
      : "#FFF"};
  color: ${(props) =>
    props.$primary ? "#FFF" : props.$danger ? "#FFF" : "#93c5fd"};
  box-shadow: ${(props) => (props.$animation ? "0.4em 0.4em 0 0 #667eea" : "")};

  text-decoration: ${[(props) => (props.$line ? "line-through" : "none")]};

  &:hover {
    cursor: pointer;
    opacity: ${(props) => (props.$animation ? "1" : "0.7")};
    box-shadow: ${(props) =>
      props.$animation ? "0.6em 0.6em 0 0 #667eea" : ""};
    transition: 0.3s ease-in;
  }

  font-family: "Montserrat", sans-serif;
  font-size: 0.9em;
  border: none;
  padding: ${(props) => (props.$py ? `${props.$py}em` : "1em")}
    ${(props) => (props.$px ? `${props.$px}em` : "1.1em")};
  font-weight: bold;
  border-radius: 0.5em;
`;
