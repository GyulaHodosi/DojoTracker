import React from "react";
import { HeaderTile } from "../styled-components/Reusables";

const RankingTableHeaders = ({ onClick }: { onClick: Function }) => {
    return (
        <HeaderTile
            onClick={(e) => {
                onClick(e);
            }}
        >
            <p data-reference="rank" data-testid="rank-sort-btn">
                Rank
            </p>
            <p data-reference="lastName" data-testid="name-sort-btn">
                Name
            </p>
            <p data-reference="numOfCompletedDojos" data-testid="numberofdojos-sort-btn">
                Completed dojos
            </p>
            <p data-reference="score" data-testid="score-sort-btn">
                Score
            </p>
        </HeaderTile>
    );
};

export default RankingTableHeaders;
