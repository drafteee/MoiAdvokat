import React from "react"
import "./style.css"

const GlobalLoading = () => {
	return (

	/*
	 * <div className="global-loader-background">
	 *   <div className="global-loader">
	 *     <div className="global-loading"></div>
	 *   </div>
	 * </div>
	 */
		<div className="global-loader-background">
			<span class="loader">
				<span class="loader-inner" />
			</span>
		</div>
	)
}

export default GlobalLoading
