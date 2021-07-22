import React, {
  Component 
} from 'react'

export default class ErrorBoundary extends Component {
  constructor(props) {
    super(props)
    this.state = {
    }
  }

  render() {
    return this.props.error ? <small>{ this.props.error.message }</small> : this.props.children
  }
}
