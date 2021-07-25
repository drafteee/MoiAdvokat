import React from 'react'
import './style.css'

const NotRead = ({ right = '15%', top = '' }) => {
	return (
		<div
			className="dont-read"
			style={ {
				right,
				top
			} }
		/>

	)
}
export default NotRead