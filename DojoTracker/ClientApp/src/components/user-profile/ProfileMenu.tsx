import React from "react";
import { StyledCard } from "../styled-components/Reusables";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faUser, faChartBar, faList } from "@fortawesome/free-solid-svg-icons";
import styled from "styled-components";
import { NavLink } from "react-router-dom";

const StyledProfileMenu = styled.div`
    display: flex;
    flex-direction: column;
    font-size: 1rem;
    justify-content: flex-start;
    align-items: flex-start;
    color: gray;
    width: 100%;
    div {
        padding: 1rem;

        svg {
            margin: 0 0.5rem 0 0;
        }

        &:hover {
            color: #dc3545;
            cursor: pointer;
            transition: all 0.5s;
        }
    }
`;

interface Props {}

const ProfileMenu = (props: Props) => {
    const toggleSelected = () => {};

    return (
        <StyledProfileMenu>
            <div>
                <FontAwesomeIcon icon={faUser} size="1x" />
                <span>Profile</span>
            </div>
            <div>
                <FontAwesomeIcon icon={faChartBar} size="1x" />
                <span>Statistics</span>
            </div>
            <div>
                <FontAwesomeIcon icon={faList} size="1x" />
                <span>Solutions</span>
            </div>
        </StyledProfileMenu>
    );
};

export default ProfileMenu;
