using System;
using YanZhiwei.DotNet.Core.Config;

namespace YanZhiwei.DotNet.Core.ConfigExamples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                ConfigContext _configContext = new ConfigContext();

                //CacheConfig _cacheConfig = new CacheConfig();
                //_cacheConfig.ClusteredByIndex = true;

                //_cacheConfig.CacheConfigItems = new CacheConfigItem[1];
                //_cacheConfig.CacheConfigItems[0] = new CacheConfigItem();
                //_cacheConfig.CacheConfigItems[0].Desc = "ASC";
                //_cacheConfig.CacheConfigItems[0].IsAbsoluteExpiration = true;
                //_cacheConfig.CacheConfigItems[0].Minitus = 5;
                //_cacheConfig.CacheConfigItems[0].Priority = 1;
                //_cacheConfig.CacheConfigItems[0].ProviderName = "YanZhiwei.DotNet.Core.Config";

                //_cacheConfig.CacheProviderItems = new CacheProviderItem[2];
                //_cacheConfig.CacheProviderItems[0] = new CacheProviderItem();
                //_cacheConfig.CacheProviderItems[0].Desc = "ASC";
                //_cacheConfig.CacheProviderItems[0].Type = "YanZhiwei.DotNet.Core.Config";
                //_cacheConfig.CacheProviderItems[1] = new CacheProviderItem();
                //_cacheConfig.CacheProviderItems[1].Desc = "DES";
                //_cacheConfig.CacheProviderItems[1].Type = "YanZhiwei.DotNet.Core.Config.Examples";

                //_cacheConfig.UpdateNodeList<CacheProviderItem>(_cacheConfig.CacheProviderItems);
                //_configContext.Save<CacheConfig>(_cacheConfig, "A");
                //var _cacheConfigA = _configContext.Get<CacheConfig>("A");

                //var _cacheConfigB = CachedConfigContext.Current.Get<CacheConfig>("A");

                //CabInComeConfig _cabInComeConfig = new CabInComeConfig();
                //_cabInComeConfig.ClusteredByIndex = false;
                //_cabInComeConfig.CacheConfigItems = new CabInComeConfigItem[1];
                //string _keyId = "3b0ddbf0-cdd3-4695-845a-d10ed99cab52";
                //_cabInComeConfig.CacheConfigItems[0] = new CabInComeConfigItem();
                //_cabInComeConfig.CacheConfigItems[0].CabId = Guid.NewGuid().ToString();
                //_cabInComeConfig.CacheConfigItems[0].VoltageMaxValue = 240;
                //_cabInComeConfig.CacheConfigItems[0].VoltageMinValue = 210;
                //_cabInComeConfig.CacheConfigItems[0].CurrentMaxValue = 10;
                //_cabInComeConfig.CacheConfigItems[0].CurrentMinValue = 1;
                //_cabInComeConfig.CacheConfigItems[0].KeyId = _keyId;
                //_configContext.Save<CabInComeConfig>(_cabInComeConfig);

                var _cacheConfigC = CachedConfigContext.Current.CabInComeConfig;
                _cacheConfigC.CacheConfigItems[0].CurrentMaxValue = 30;
                _configContext.Save(_cacheConfigC);
                _cacheConfigC = CachedConfigContext.Current.CabInComeConfig;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}