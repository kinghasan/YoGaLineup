# UTween - Workflow And Lifecycle

## UTweenManager Loop
* UTweenManager `Update()` / `LateUpdate()` / `FixedUpdate()` / `WaitForFixedUpdate()` / `WaitForEndOfFrame()`
* UTweenManager `UpdateImpl()`
* Foreach TweenData `UpdateInternal()`


## UTweenAnimation Component Loop
### Start
* UTweenAnimation (`Awake()` / `OnEnable()` / `OnStart()` / `Play()`)
* TweenData (`Awake()` / `OnEnable()` / `OnStart()` / `Play()`)
	* **[Editor Preview]** TweenData `RecordObject()`
* With `Sample` option is on
	* TweenData `PreSample()`
	* Foreach Tweener `PreSample()`
	* TweenData `Sample(0f)`
* TweenData `Play()`
	* **[Runtime]** UTweenManager `AddTweenData()`
	* **[Editor Preview]** UTweenAnimation `PreviewStart()`
* TweenData `Initialize()` called from UTweenManager

### **[Editor Preview]** Playing
* UTweenAnimation `EditorUpdate()`
* UTweenAnimation `EditorUpdateImpl()`
* TweenData `UpdateInternal()`
* TweenData `Update()`
* TweenData `Sample()`
* Foreach Tweener `GetFactor()`
* Foreach Tweener `Sample(factor)`
* Foreach Tweener `SetDirty()`

### Stop
* UTweenAnimation (`OnDisable()` / `Stop()`)
* TweenData `OnDisable()`
* TweenData `Stop()`
	* **[Runtime]** UTweenManager `RemoveTweenData()`
	* **[Editor Preview]** UTweenAnimation `PreviewEnd()`


## UTween API Flow
* UTween Quick API / Component Extension API
* UTween `CreateTweenData()`
* TweenData `Play()`
* UTweenManager `AddTweenData()`