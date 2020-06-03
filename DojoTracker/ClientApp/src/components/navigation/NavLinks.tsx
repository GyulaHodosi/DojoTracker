import React, { useContext } from "react";
import styled from "styled-components";
import { CustomLink, CustomNavlink } from "../styled-components/Reusables";
import { NavLink } from "react-router-dom";

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
            background: #dc3545;
            height: 2px;
            transition: all 0.5s;
        }

        &:hover::after {
            width: 100%;
        }

        & .nav-active {
            color: #dc3545;
            font-weight: bold;
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
            <p id="dojo-link-nav">
                <CustomNavlink to="/dojos" activeClassName="nav-active">
                    Dojos
                </CustomNavlink>
            </p>
            <p id="ranking-link-nav">
                <CustomNavlink to="/ranking" activeClassName="nav-active">
                    Ranking
                </CustomNavlink>
            </p>
            <p id="profile-link-nav">
                <CustomNavlink to="/user/profile" activeClassName="nav-active">
                    Profile
                </CustomNavlink>
            </p>
        </StyledNavLinks>
    );
};

export default NavLinks;
