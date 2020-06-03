import React from "react";
import styled from "styled-components";
import { CustomLink } from "../styled-components/Reusables";

interface StyleProps {
    mobile: boolean;
}

const StyledNavLinks = styled.ul`
    text-align: center;
    list-style: none;
    text-decoration: none;
    justify-content: center;
    align-items: center;
    display: flex;
    margin: 0;
    padding: 0;
    flex-direction: ${(props: StyleProps) => (props.mobile ? "column" : "row")};

    & p {
        float: ${(props: StyleProps) => (props.mobile ? "" : "left")};
        margin: ${(props: StyleProps) => (props.mobile ? "1rem 0" : "0 0.5rem")};
        padding: ${(props: StyleProps) => (props.mobile ? "0.5rem 0" : "0 0.5rem")};
        letter-spacing: 0.2rem;
        position: relative;
        font-size: 0.9rem;

        &:hover {
            cursor: pointer;
        }

        &::after {
            position: absolute;
            top: 25px;
            bottom: 0;
            left: 0;
            right: 0;
            margin: auto;
            width: 0%;
            content: ".";
            color: transparent;
            background: black;
            height: 2px;
            transition: all 0.5s;
        }

        &:hover::after {
            width: 100%;
        }
    }

    @media (max-width: 1280px) {
        display: ${(props: StyleProps) => (props.mobile ? "" : "none")};
    }
`;

interface Props {
    mobile: boolean;
}

const NavLinks = (props: Props) => {
    return (
        <StyledNavLinks mobile={props.mobile}>
            <p data-testid="dojo-link-nav">
                <CustomLink to="/dojos">Dojos</CustomLink>
            </p>
            <p data-testid="ranking-link-nav">
                <CustomLink to="/ranking">Ranking</CustomLink>
            </p>
            <p
                onClick={() => {
                    window.open("https://www.youtube.com/watch?v=DKP16d_WdZM", "_blank");
                }}
                data-testid="profile-link-nav"
            >
                Profile
            </p>
        </StyledNavLinks>
    );
};

export default NavLinks;
