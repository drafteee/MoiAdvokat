const merge = require('webpack-merge')
const common = require('./webpack.common.js')
const webpack = require('webpack')
const OptimizeCSSAssetsPlugin = require('optimize-css-assets-webpack-plugin')
const MiniCssExtractPlugin = require('mini-css-extract-plugin')
const TerserJSPlugin = require('terser-webpack-plugin')
const Global = require('./src/Global')

module.exports = merge(common, {
  mode: 'production',
  devtool: 'source-map',
  //тест прода на локальном
  // devServer: {
  //   contentBase: './dist',
  //   hot: true,
  //   stats: {
  //     chunks: false
  //   },
  //   //обращение к .js и .css файлам через .gz файлы
  //   before: function(app, server) {
  //     app.get('*.js', function(req, res, next) {
  //       req.url = req.url + '.gz'
  //       res.set('Content-Encoding', 'gzip')
  //       res.set('Content-Type', 'text/javascript')
  //       next()
  //     })
  //     app.get('*.css', function(req, res, next) {
  //       req.url = req.url + '.gz'
  //       res.set('Content-Encoding', 'gzip')
  //       res.set('Content-Type', 'text/css')
  //       next()
  //     })
  //   }
  // },
  module: {
    rules: [
      {
        test: /\.(css|scss)$/,
        use: [ MiniCssExtractPlugin.loader, 'css-loader' ],
      },
    ],
  },
  optimization: {
    minimizer: [ 
      new TerserJSPlugin({}), 
      new OptimizeCSSAssetsPlugin({}) 
    ],
    minimize: true,
    runtimeChunk: {
      name: (entrypoint) => `runtimechunk~${entrypoint.name}`,
    },
    moduleIds: 'hashed',
    splitChunks: {
      //максимальный размер чанка (в теории) в байтах
      maxSize: 409600,
      minSize: 100000,
      cacheGroups: {
        //default: false, // disable the built-in groups, default & vendors (vendors is overwritten below)
        //node_modules/react-dom
        reactDom: {
          test: /[\\/]node_modules[\\/]react-dom[\\/]/,
          name: 'react-dom',
          //синхронные и асинхронные чанки
          chunks: 'all',
        },
        //node_modules/react for IE - not work
        // react: {
        //     test: /[\\/]node_modules[\\/]((react).*)[\\/]/,
        //     name: "react",
        //     chunks: "all"
        // },
        //others in node_modules
        // vendor: {
        //   // name of the chunk
        //   name: 'vendor',
        //   // async + async chunks
        //   chunks: 'initial',
        //   // import file path containing node_modules
        //   test: /node_modules/,
        //   // priority
        //   priority: 20,
        //   reuseExistingChunk: true,
        // },
        antd: {
          test: /[\\/]node_modules[\\/](antd|@ant-design)[\\/]/,
          name: 'antd',
          reuseExistingChunk: true,
          chunks: 'all',
          priority: 0,
        },
        vendor: {
          test: /[\\/]node_modules[\\/](react|react-dom|@lingui|react-router|react-router-dom)[\\/]/,
          name: 'vendor',
          reuseExistingChunk: true,
          chunks: 'all',
          priority: 20,
        },
        common: {
          // test: /[\\/]node_modules[\\/]((?!react).*)[\\/]/,
          // name: 'common([name])',
          // chunks: 'all',
          // //минимальный размер чанка (в теории) в байтах
          // minSize: 204800
          name: 'common',
          minChunks: 2,
          chunks: 'async',
          priority: 10,
          reuseExistingChunk: true,
          enforce: true,
        },
        // styles: {
        //   name: 'styles',
        //   test: /\.+(css)$/,
        //   chunks: 'all'
        // },
      },
    },
  },
  plugins: [
    new webpack.DefinePlugin({
      'process.env': {
        NODE_ENV: JSON.stringify('production'),
        SERVER_API_URL: JSON.stringify(Global.SERVER_API_URL),
      },
    }),
    new webpack.HashedModuleIdsPlugin(),
  ],
})
