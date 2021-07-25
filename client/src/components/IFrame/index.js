import React, { useState, useEffect } from 'react'
import { createPortal } from 'react-dom'

const IFrame = ({
    content,
    ...props
}) => {
    const [contentRef, setContentRef] = useState(null)
    const mountNode = contentRef?.contentWindow?.document?.body?.innerHTML

    useEffect(() => {
        if (content) {
            // contentRef.src = '/'
            contentRef.contentWindow.document.body.innerHTML = content
        }
    }, [content])

    return (
        <iframe {...props} ref={setContentRef}>
            {/* {mountNode && createPortal(content, mountNode)} */}
        </iframe>
    )
}

export default IFrame;