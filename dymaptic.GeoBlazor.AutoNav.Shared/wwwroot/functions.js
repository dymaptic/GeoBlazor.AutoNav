window.registerThemeWatcher = (dotNetRef) => {
    // Create a MediaQueryList object for dark mode
    const darkModeQuery = window.matchMedia("(prefers-color-scheme: dark)");
    
    let callback = async () => await dotNetRef.invokeMethodAsync('DarkModeToggled', darkModeQuery.matches);
    
    let _ = callback();

    darkModeQuery.addEventListener("change", callback);
}
