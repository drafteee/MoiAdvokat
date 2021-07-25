import Button from 'antd/es/button'
import React from 'react'
import './style.scss'
export const CustomButton = (props) => {
	const { type, children } = props

	const colorType = type ?? ''

	return (
		<Button className={ `custom-button ${ colorType }` }>
			{ children }
		</Button>
	)
}
export default CustomButton