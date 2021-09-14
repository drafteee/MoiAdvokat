import React, {useEffect} from "react"
import {
	Switch, Route, Redirect
} from "react-router-dom"
import { useSelector, useDispatch } from "react-redux";
import {
	userActions
}from "./store/actions"
import routes from './routesArray'

const Routes = () => {
	const user = useSelector(state => state.userReducer.user)
	const dispatch = useDispatch();
	useEffect(() => {
		if (user === undefined)
			dispatch(userActions.refreshUserData())
	}, [])

	const renderRoute = (path, component) => 
		<Route
			exact
			component={component}
			path={path}
		/>

	return (
		<Switch>
			{
				routes.map((route)=>{
					if(route.protect)
					{
						if(route.protect(user))
							return renderRoute(route.path, route.component)
						else if(route.redirectTo)
							return renderRoute(route.path, route.redirectTo)
					}

					if(!route.protect){
						return renderRoute(route.path, route.component)
					}
				})
			}
		</Switch>
	)
}
export default Routes
