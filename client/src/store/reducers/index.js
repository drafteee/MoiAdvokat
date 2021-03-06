import {
	combineReducers
}from "redux"
import {
	routerReducer
}from "react-router-redux"

import userReducer from "./user"
import globalReducer from "../../containers/Menu/reducer"
import homeReducer from "../../containers/Home/store/reducer"
import mobileReducer from "../../containers/MobilePhoneNumber/store/reducer"
import lawyerReducer from "../../containers/Lawyers/store/reducer"
import orderReducer from "../../containers/Orders/store/reducer"
import fileReducer from "../../containers/UploadFile/store/reducer"
import chatReducer from "./chatReducer";
import baseReducer from '../../containers/Admin/Base/store/reducer'
import addressReducer from '../../containers/AddressInputForm/store/reducer'
import orderStatusesReducer from '../../containers/Orders/Status/store/reducer'
import specializationsReducer from '../../containers/Lawyers/Specialization/store/reducer'

export default combineReducers({
	router: routerReducer,
	userReducer,
	globalReducer,
	mobileReducer,
	lawyerReducer,
	orderReducer,
	homeReducer,
	fileReducer,
	chatReducer,
	baseReducer,
	addressReducer,
	orderStatusesReducer,
	specializationsReducer
})
