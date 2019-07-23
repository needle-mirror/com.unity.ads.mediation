# Unity Ads Mediation Purchasing Adaptor

### What is it
An adaptor for using purchasing in projects that use mediation.

### How it works
* Import the unitypackage into your project
* Initialize the purchasing adaptor:
	* UnityAdsPurchasing.Initialize(IStoreLisener, ConfigurationBuilder)

Initialization requires you to pass two objects in.  The first is a object that implements the IStoreListener interface, the second is a ConfigurationBuilder that represenst the catalog and store information.

### How to build
Requirements:
* UnityEngine.Purchasing.dll is sitting in the root of the folder `BuildTemplate~/` which is compatible with the purchasing adaptor being built.  By default the dll is included but may need to be replaced if new methods are exposed
* Unity Engine installed at `/Applications/Unity`

Run the script `BuildTemplate~/build-purchasing-adaptor.sh` to build the unity package.  By default the structure is a packman package and could be deployed as such using yamato.  Currently it is only scheduled to be released as an asset store package.