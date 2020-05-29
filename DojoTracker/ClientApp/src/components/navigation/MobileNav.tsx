import React, { useContext, Fragment } from "react";
import styled from "styled-components";
import { LoginContext } from "../context/LoginContextProvider";
import NavLinks from "./NavLinks";
import LogOut from "../user-management/LogOut";
import SignIn from "../user-management/SignIn";

const StyledMobileNav = styled.nav`
    position: fixed;
    top: 0;
    right: 0;
    bottom: 0;
    left: 0;
    transition: opacity 0.5s;
    background-color: #fff;
    opacity: 0;
    pointer-events: none;
    display: flex;
    flex-direction: column;
    width: 100%;
    height: 100%;
    justify-content: center;
    align-items: center;

    &.is-visible {
        opacity: 1 !important;
        z-index: 890 !important;
        pointer-events: all;
    }

    @media (min-width: 1280px) {
        display: none;
    }

    & button {
        margin: 1rem;
    }
`;

interface Props {}

const MobileNav = (props: Props) => {
    const { isLoggedIn } = useContext(LoginContext);

    const hideMenu = () => {
        document.querySelector(".mobile-nav")?.classList.toggle("is-visible");
        document.querySelector(".burger-menu")?.classList.toggle("is-opened");
    };

    return (
        <StyledMobileNav className="mobile-nav" onClick={() => hideMenu()}>
            <nav>
                <ul>
                    {isLoggedIn ? (
                        <Fragment>
                            <NavLinks mobile={true} />
                            <div className="std-log">
                                <LogOut />
                            </div>
                        </Fragment>
                    ) : (
                        <div className="std-log">
                            <SignIn />
                        </div>
                    )}
                </ul>
            </nav>
        </StyledMobileNav>
    );
};

export default MobileNav;
