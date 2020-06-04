import React, { useContext } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faUser, faChartBar, faList } from "@fortawesome/free-solid-svg-icons";
import styled from "styled-components";
import { ProfilePageContext } from "../context/ProfilePageContextProvider";

const StyledProfileMenu = styled.div`
    display: flex;
    flex-direction: column;
    font-size: 1rem;
    justify-content: center;
    align-items: center;
    text-align: left;
    color: gray;
    width: 100%;
    transition: color 0.5s;
    div {
        padding: 1rem;

        svg {
            margin: 0 0.5rem 0 0;
        }

        &:hover {
            color: #dc3545;
            cursor: pointer;
        }

        &.active {
            color: #dc3545;
        }
    }
`;

interface Props {}

const ProfileMenu = (props: Props) => {
    const { setProfileState } = useContext(ProfilePageContext);

    const toggleSelected = (event: any) => {
        if (event?.target?.dataset?.menureference !== undefined) {
            setProfileState(event.target.dataset.menureference);
        }
        return;
    };

    return (
        <StyledProfileMenu
            onClick={(event) => {
                toggleSelected(event);
            }}
        >
            <div data-menureference="profile">
                <FontAwesomeIcon icon={faUser} size="1x" data-menureference="profile" />
                <span data-menureference="profile">Profile</span>
            </div>
            <div data-menureference="statistics">
                <FontAwesomeIcon icon={faChartBar} size="1x" data-menureference="statistics" />
                <span data-menureference="statistics">Statistics</span>
            </div>
            <div data-menureference="solutions">
                <FontAwesomeIcon icon={faList} size="1x" data-menureference="solutions" />
                <span data-menureference="solutions">Solutions</span>
            </div>
        </StyledProfileMenu>
    );
};

export default ProfileMenu;
