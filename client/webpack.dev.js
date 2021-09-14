const merge = require('webpack-merge')
const common = require('./webpack.common.js')
const webpack = require('webpack')
const Global = require('./src/Global')
module.exports = merge(common, {
	mode      : 'development',
	devtool   : 'inline-source-map',
	devServer : {
		static : './dist',
		// hot         : true,
		host: 'localhost',
    port: '3000',
		// stats       : {
		// 	chunks: false
		// },

		//обращение к .js и .css файлам через .gz файлы
		// onBeforeSetupMiddleware : function (app, server){
		// 	app.get('*.js', function (req, res, next){
		// 		req.url += '.gz'
		// 		res.set('Content-Encoding', 'gzip')
		// 		res.set('Content-Type', 'text/javascript')
		// 		next()
		// 	})
		// 	app.get('*.css', function (req, res, next){
		// 		req.url += '.gz'
		// 		res.set('Content-Encoding', 'gzip')
		// 		res.set('Content-Type', 'text/css')
		// 		next()
		// 	})
		// }
	},
	module: {
		rules: [
			{
				test : /\.(css)$/,
				use  : ['style-loader', 'css-loader']
			}

		]

	},
	resolve: {
		alias: {
			'react-dom': '@hot-loader/react-dom'
		}
	},

	plugins: [
		// new webpack.HotModuleReplacementPlugin(),
		new webpack.DefinePlugin({
			'process.env': {
				NODE_ENV       : JSON.stringify('development'),
				SERVER_API_URL : JSON.stringify(Global.SERVER_API_URL),
				FAST_REFRESH: (!!process.env.FAST_REFRESH) || false
			}
		})
	]
})