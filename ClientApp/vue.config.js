module.exports = {
  productionSourceMap: false,
  transpileDependencies: ["vuetify"],
  chainWebpack: config => config.optimization.minimize(true),
};
