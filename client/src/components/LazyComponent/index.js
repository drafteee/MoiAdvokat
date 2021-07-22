import React, {
	useEffect, useCallback
}from 'react'
import useIO from '../../hooks/useIO'

const LazyComponent = ({
	children
}) => {
	const [
		observer,
		setElement,
		entries
	] = useIO({
		threshold : 0.25,
		root      : null
	})

	// //console.log('render')

	const setRef = useCallback((node) => {
		//console.log(node)
		setElement(node)
	}, [setElement])

	useEffect(() => {
		entries.forEach((entry) => {
			if (entry.isIntersecting){
				const block = entry.target

				// //console.log(block)
				observer.unobserve(block)
			}
		})
	}, [entries, observer])

	// //console.log(entries)
	const child = (
		<div ref={ setRef }>
			{ entries.length > 0
				? entries[0].isIntersecting
					? children
					: null
				: null }
		</div>
	)

	return child
}

export default LazyComponent
