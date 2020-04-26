<template>
  <div class="bodyparts">
    <div v-for="bp in bodyparts" :key="bp.id" class="bodypart">
      <img style="max-width: 100px;" :src="bp.imageUrl" alt="">
      <p><b>{{ bp.name }}</b></p>
      <p>Location: {{ bp.location }}</p>
      <vs-button @click="showExercises(bp.exercises)">Show exercises</vs-button>
    </div>

    <vs-dialog v-model="showDialog" :loading="loading">
      <div v-for="e in dialogExercises" :key="e.id">
        <p id="name"><b>{{ e.name }}</b></p>
        <p id="description">{{ e.description }}</p>
        <a :href="e.videoUrl" target="_blank">Watch video</a>
      </div>
    </vs-dialog>
  </div>
</template>

<script>

export default {
  name: 'Exercises',
  data:() => ({
    bodyparts: [],
    dialogExercises: [],
    showDialog: false,
    loading: true
  }),
  methods: {
    getBodyParts() {
      fetch(
        "https://" + window.location.hostname + ":44369/api/v1/bodyparts",
        ).then((res) => {
          return res.json()
      }).then((data) => {
        if (data != null) {
          data.forEach(e => {
            this.bodyparts.push(e);
          });
        }
      })
    },
    async showExercises(exercises) {
      this.loading = true;
      this.dialogExercises = [];
      this.showDialog = true;
      for (const tp of exercises) {
        let res = await fetch("https://" + window.location.hostname + ":44369/api/v1/exercises/" + tp.exerciseId);
        let bp = await res.json();
        this.dialogExercises.push(bp);
      };
      this.loading = false;
    },
  },
  mounted() {
    this.getBodyParts();
  }
}
</script>

<style lang="scss" scoped>
.bodypart {
  padding: 2rem;
  border-radius: 2rem;

  &:nth-child(odd) {
    background-color: #f5f5f5;
  }

  .vs-button {
    margin: 0;
    margin-top: 1rem;
  }
}
</style>