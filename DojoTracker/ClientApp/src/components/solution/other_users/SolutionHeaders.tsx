import React from "react";
import { HeaderTile } from "../../styled-components/Reusables";

interface Props {
    onClick: Function;
}

const SolutionHeaders = (props: Props) => {
    return (
        <HeaderTile
            onClick={(event) => {
                props.onClick(event);
            }}
        >
            <p data-reference="">User</p>
            <p data-reference="language">Language</p>
            <p data-reference="submissionDate">Submission date</p>
            <p data-reference="">Solution</p>
        </HeaderTile>
    );
};

export default SolutionHeaders;
