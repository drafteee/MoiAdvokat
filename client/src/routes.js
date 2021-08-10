import React, {
	useEffect, useState
}from "react"
import {
	Switch, Route
}from "react-router-dom"
import { homeLoadables, accountLoadables, BLLoadables }from "./loadables"
import NotFound from "./components/NotFound"
import {
	useSelector, useDispatch
}from "react-redux"

const Routes = () => {
	return (
		<Switch>
			<Route
				exact
				component={ homeLoadables.LoadableHome }
				path="/"
			/>
			<Route
				exact
				component={ accountLoadables.LoadableLogin }
				path="/login"
			/>
			<Route
				exact
				component={ accountLoadables.LoadableAccount }
				path="/account"
			/>
			<Route
				exact
				component={ BLLoadables.LoadableLawyersList }
				path="/lawyers"
			/>
			<Route
				exact
				component={ BLLoadables.LoadableOrdersList }
				path="/orders"
			/>
			<Route
				exact
				component={ BLLoadables.LoadableOrder }
				path="/order"
			/>
			<Route
				exact
				component={ NotFound }
				path="*"
			/>
		</Switch>
	)
}
export default Routes
