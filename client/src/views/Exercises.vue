<template>
  <div class="exercises">
    <div v-for="e in exercises" :key="e.id" class="exercise">
      <p id="name"><b>{{ e.name }}</b></p>
      <p id="description">{{ e.description }}</p>
      <a :href="e.videoUrl" target="_blank">Watch video</a>
      <vs-button @click="showBodyParts(e.targetParts)">Show affected body parts</vs-button>
      <vs-button class="delete" @click="deleteExercise(e.id)">Delete</vs-button>
    </div>

    <vs-dialog v-model="showDialog" :loading="loading">
      <div v-for="bp in dialogBodyParts" :key="bp.id" class="body-part">
        <img style="max-width: 100px;" :src="bp.imageUrl" alt="">
        <p><b>{{ bp.name }}</b></p>
        <p>Location: {{ bp.location }}</p>
      </div>
    </vs-dialog>
  </div>
</template>

<script>

export default {
  name: 'Exercises',
  data:() => ({
    exercises: [],
    dialogBodyParts: [],
    showDialog: false,
    loading: true
  }),
  methods: {
    getExercises() {
      fetch(
        "https://" + window.location.hostname + ":44369/api/v1/exercises",
        ).then((res) => {
          return res.json()
      }).then((data) => {
        if (data != null) {
          data.forEach(e => {
            this.exercises.push(e);
          });
        }
      })
    },
    async showBodyParts(targetParts) {
      this.loading = true;
      this.dialogBodyParts = [];
      this.showDialog = true;
      for (const tp of targetParts) {
        let res = await fetch("https://" + window.location.hostname + ":44369/api/v1/bodyparts/" + tp.bodyPartId);
        let bp = await res.json();
        this.dialogBodyParts.push(bp);
      };
      this.loading = false;
    },
    async deleteExercise(id) {
      let res = await fetch("https://" + window.location.hostname + ":44369/api/v1/exercises/" + id,
      {
        method: 'DELETE',
      });
      this.exercises = [];
      this.getExercises();
    }
  },
  mounted() {
    this.getExercises();
  }
}
</script>

<style lang="scss" scoped>
.exercise {
  padding: 2rem;
  border-radius: 2rem;

  &:nth-child(odd) {
    background-color: #f5f5f5;
  }

  .vs-button {
    margin: 0;
    margin-top: 1rem;
  }

  .delete {
    background-color: red;
  }
}
</style>