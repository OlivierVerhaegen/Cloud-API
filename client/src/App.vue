<template>
  <div id="app">
    <nav id="nav">
      <div>
        <router-link to="/">Home</router-link> |
        <router-link to="/exercises">Exercises</router-link> |
        <router-link to="/bodyparts">Body parts</router-link> |
        <router-link to="/myexercises">My exercises</router-link>
      </div>
      
    </nav>
    <router-view/>
  </div>
</template>

<style lang="scss">
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}

#nav {
  display: flex;
  justify-content: space-between;
  padding: 1rem;

  a {
    font-weight: bold;
    color: #2c3e50;

    &.router-link-exact-active {
      color: RGB(var(--vs-primary));
    }
  }
}
</style>
