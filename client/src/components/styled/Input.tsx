import styled, { css } from "styled-components";

export const Input = styled.input<{ $px?: number; $py?: number; }>`
    font-size: 1.3em;
    padding: ${props => props.$py ? `${props.$py}em` : "0.4em"} ${props => props.$px ? `${props.$px}em` : "0.4em"};
    max-width: 220px;
    border-radius: 0.3em;
    border: 3px solid #667eea;
    outline: none;

    &:focus {
        border-color: #93c5fd;
        transition: border-color .3s ease-in;
        color: #667eea;
    }
`