const errorMiddleware = (store) => (next) => (action) => {
	if (action.payload?.err) {

	}
	next(action)
}

export default errorMiddleware
