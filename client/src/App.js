import React, {
	useEffect
}from "react"
import {
	useSelector
}from "react-redux"
import {
	hot, setConfig
}from "react-hot-loader/root"
import {
	Router, useRouteMatch
}from "react-router-dom"

import history from "./helpers/history"

const App = () => {
	return (
		<>
Hello
		</>
	)
}

export default hot(App)
