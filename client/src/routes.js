import React, {
	useEffect, useState
}from "react"
import {
	Switch, Route
}from "react-router-dom"
import { }from "./loadables"
import NotFound from "./components/NotFound"
import {
	useSelector, useDispatch
}from "react-redux"

const Routes = () => {
	return (
		<Switch>
			<Route
				exact
				component={ NotFound }
				path="*"
			/>
		</Switch>
	)
}
export default Routes
