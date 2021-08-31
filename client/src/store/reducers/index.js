import {
	combineReducers
}from "redux"
import {
	routerReducer
}from "react-router-redux"

import userReducer from "./user"
import globalReducer from "../../containers/Menu/reducer"
import mobileReducer from "../../containers/MobilePhoneNumber/store/reducer"
import lawyerReducer from "../../containers/Lawyers/store/reducer"
import orderReducer from "../../containers/Orders/store/reducer"
import fileReducer from "../../containers/UploadFile/store/reducer"

export default combineReducers({
	router: routerReducer,
	userReducer,
	globalReducer,
	mobileReducer,
	lawyerReducer,
	orderReducer,
	fileReducer
})
