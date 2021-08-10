import React from "react"
import {
	Row
}from "antd"
import {
	useSelector
}from "react-redux"
import "./style.scss"

const LocalLoading = (props) => {
	const { isGlobalLoadingVisible } = useSelector((state) => state.globalReducer)
	return (
		<>
			{ !isGlobalLoadingVisible &&

        /*
         * <Row className="row-loader" justify="center" align="middle">
         *   <div className="local-loading"></div>
         *   <h4>{props.text}</h4>
         * </Row>
         */
        <div class="loader-data">
        	<div class="loader-back">
        		<div class="loader-rect">
        			<div class="loader-lines-hor">
        				<div />
        				<div />
        				<div />
        				<div />
        				<div />
        			</div>
        			<div class="loader-lines-ver">
        				<div />
        				<div />
        				<div />
        				<div />
        				<div />
        			</div>
        		</div>
        	</div>
        	<div class="loader-white">
        		<div class="loader-white-lines">
        			<div />
        			<div />
        			<div />
        			<div />
        			<div />
        		</div>
        	</div>
        </div> }
		</>
	)
}

export default LocalLoading
