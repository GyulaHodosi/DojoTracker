import React, { useState, useEffect, useContext, Fragment } from "react";
import { IUserStatistics } from "../../static/util/interfaces";
import { useLocation } from "react-router-dom";
import { UserDataContext } from "../context/UserDataContextProvider";
import queryString from "query-string";
import { Container, ContainerWithRows } from "../styled-components/Reusables";
import ProfileCard from "./ProfileCard";
import ProfileMenu from "./ProfileMenu";
import ProfileData from "./ProfileData";
import styled from "styled-components";

const StyledProfilePage = styled(ContainerWithRows)`
    justify-content: space-between;
    align-items: flex-start;
    width: 80%;
    height: 100%;
`;

const SmallContainer = styled(Container)`
    min-width: 10%;
    width: 20%;
`;

interface Props {}

const ProfileContainer = (props: Props) => {
    const [userData, setUserData] = useState<any | IUserStatistics>();
    const [isCurrentUser, setIsCurrentUser] = useState<any | boolean>();
    const { user, getUserDataById } = useContext(UserDataContext);

    const location = useLocation();

    useEffect(() => {
        (async function getUserData() {
            const requestedUserId = queryString.parse(location.search).id;

            if (requestedUserId === undefined) {
                setUserData(user);
                console.log(user);
                setIsCurrentUser(true);
            } else {
                const otherUser = await getUserDataById(requestedUserId);
                setUserData(otherUser);
                setIsCurrentUser(false);
            }
        })();
    }, [user]);

    return (
        <StyledProfilePage>
            {userData && (
                <Fragment>
                    <SmallContainer>
                        <ProfileCard user={userData} />
                        <ProfileMenu />
                    </SmallContainer>
                    <ProfileData />
                </Fragment>
            )}
        </StyledProfilePage>
    );
};

export default ProfileContainer;
