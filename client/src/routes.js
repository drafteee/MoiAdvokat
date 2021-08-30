import React from "react"
import {
	Switch, Route
} from "react-router-dom"
import { homeLoadables, accountLoadables, BLLoadables } from "./loadables"
import NotFound from "./components/NotFound"
import { OrdersLoadables } from "./loadables/orders"

const Routes = () => {
	return (
		<Switch>
			<Route
				exact
				component={homeLoadables.LoadableHome}
				path="/"
			/>
			<Route
				exact
				component={accountLoadables.LoadableLogin}
				path="/login"
			/>
			<Route
				exact
				component={accountLoadables.LoadableAccount}
				path="/account"
			/>

			<Route
				exact
				component={BLLoadables.LoadableLawyersList}
				path="/lawyers"
			/>
			<Route
				exact
				component={BLLoadables.LoadableOrdersList}
				path="/orders"
			/>
			<Route
				exact
				component={BLLoadables.LoadableOrder}
				path="/order/:id"
			/>

			<Route
				exact
				component={ BLLoadables.LoadableReport }
				path="/report"
			/>


			<Route
				exact
				component={OrdersLoadables.LoadableSubmitOrder}
				path="/submitOrder"
			/>

			<Route
				exact
				component={NotFound}
				path="*"
			/>
		</Switch>
	)
}
export default Routes
